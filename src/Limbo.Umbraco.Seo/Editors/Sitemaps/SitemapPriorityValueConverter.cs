using System;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

public class SitemapPriorityValueConverter : PropertyValueConverterBase {

    public override bool IsConverter(IPublishedPropertyType propertyType) {
        return propertyType.EditorAlias == SitemapPriorityEditor.EditorAlias;
    }

    public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
        return typeof(float?);
    }

    public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
        return PropertyCacheLevel.Element;
    }

    public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) {
        // [CHANGE: the editor now submits a JSON number rather than the string the AngularJS editor sent,
        // so a "Decimal" property may surface as decimal/double - without these cases the priority would
        // silently fall through to null] Related: Extensions/PublishedContentExtensions.cs, Sites/SiteAccessor.cs
        return source switch {
            float f => f,
            double d => (float) d,
            decimal m => (float) m,
            string str => float.TryParse(str, CultureInfo.InvariantCulture, out float result) ? result : null,
            _ => null
        };
    }

}