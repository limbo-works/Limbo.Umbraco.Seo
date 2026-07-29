using Umbraco.Cms.Core.Packaging;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Migrations;

// [CHANGE: Umbraco 17 upgrade - property editor UI aliases had to change] Related: Migrations/UpdatePropertyEditorUiAliases.cs, Editors/PreviewEditor.cs, Client/src/manifests.ts
/// <summary>
/// Package migration plan for this package. Umbraco discovers the plan on its own and runs any
/// pending migrations at startup, so the plan doesn't need to be registered in
/// <see cref="Composers.SeoComposer"/>.
/// </summary>
/// <remarks>The package name must match the <c>name</c> in <c>wwwroot/umbraco-package.json</c>.</remarks>
public class SeoMigrationPlan : PackageMigrationPlan {

    public SeoMigrationPlan() : base("Limbo.Umbraco.Seo") { }

    protected override void DefinePlan() {
        To<UpdatePropertyEditorUiAliases>("13ab0d3a-b8e3-4d3d-9c6e-4a0f2d9b7c11");
    }

}
