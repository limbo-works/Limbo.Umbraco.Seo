using System.Collections.Generic;
using System.Threading.Tasks;
using Limbo.Umbraco.Seo.Editors;
using Limbo.Umbraco.Seo.Editors.Sitemaps;
using Umbraco.Cms.Infrastructure.Migrations;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Migrations;

// [CHANGE: Umbraco 17 upgrade - property editor UI aliases had to change] Related: Migrations/SeoMigrationPlan.cs, Editors/PreviewEditor.cs, Client/src/constants.ts
/// <summary>
/// Points existing data types at the property editor UI aliases introduced with the Umbraco 17
/// version of this package.
/// </summary>
/// <remarks>
/// <para>Umbraco 13 had no concept of a property editor UI, and the core upgrade only adds the
/// <c>propertyEditorUiAlias</c> column without filling it in. The Management API then falls back to
/// reporting the editor alias as the UI alias, which no longer resolves to anything now that the
/// schema and UI aliases differ - leaving the editor as "Missing Property Editor" in the
/// backoffice.</para>
/// <para>Rows that already point at a UI alias other than the old editor alias are left alone, so
/// a site that has deliberately swapped in its own UI keeps it.</para>
/// </remarks>
public class UpdatePropertyEditorUiAliases : AsyncMigrationBase {

    /// <summary>
    /// The editor aliases of this package, mapped to the UI alias each one should now use.
    /// </summary>
    private static readonly Dictionary<string, string> _uiAliases = new() {
        { PreviewPropertyEditor.EditorAlias, PreviewPropertyEditor.EditorUiAlias },
        { SitemapFrequencyPropertyEditor.EditorAlias, SitemapFrequencyPropertyEditor.EditorUiAlias },
        { SitemapPriorityPropertyEditor.EditorAlias, SitemapPriorityPropertyEditor.EditorUiAlias }
    };

    public UpdatePropertyEditorUiAliases(IMigrationContext context) : base(context) { }

    protected override Task MigrateAsync() {

        string table = SqlSyntax.GetQuotedTableName("umbracoDataType");
        string editorAlias = SqlSyntax.GetQuotedColumnName("propertyEditorAlias");
        string uiAlias = SqlSyntax.GetQuotedColumnName("propertyEditorUiAlias");

        string sql = $"UPDATE {table} SET {uiAlias} = @1 WHERE {editorAlias} = @0 AND ({uiAlias} IS NULL OR {uiAlias} = @0);";

        foreach (KeyValuePair<string, string> pair in _uiAliases) {
            Database.Execute(sql, pair.Key, pair.Value);
        }

        return Task.CompletedTask;

    }

}