using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

#pragma warning disable CS1591

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {

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
            return source switch {
                float _ => source,
                string str => float.TryParse(str, out float result) ? result : default(float?),
                _ => default(float?)
            };
        }

    }

}