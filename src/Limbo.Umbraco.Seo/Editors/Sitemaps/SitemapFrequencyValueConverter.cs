using System;
using Limbo.Umbraco.Seo.Models.Sitemaps;
using Skybrud.Essentials.Enums;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Limbo.Umbraco.Seo.Editors.Sitemaps {
    
    public class SitemapFrequencyValueConverter : PropertyValueConverterBase {
        
        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == SitemapFrequencyEditor.EditorAlias;
        }

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            return typeof(SitemapChangeFrequency);
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
            return PropertyCacheLevel.Element;
        }

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
            return EnumUtils.ParseEnum(source as string, SitemapChangeFrequency.Unspecified);
        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return inter;
        }

    }

}