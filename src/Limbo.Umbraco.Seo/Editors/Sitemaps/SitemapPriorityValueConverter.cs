using System;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps;

public class SitemapPriorityValueConverter : PropertyValueConverterBase {

    public override bool IsConverter(IPublishedPropertyType propertyType) {
        return propertyType.EditorAlias == SitemapPriorityPropertyEditor.EditorAlias;
    }

    public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
        return typeof(float?);
    }

    public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
        return PropertyCacheLevel.Element;
    }

    public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) {
        return source switch {
            float f => f,
            double d => (float) d,
            decimal m => (float) m,
            string str => float.TryParse(str, CultureInfo.InvariantCulture, out float result) ? result : null,
            _ => null
        };
    }

}