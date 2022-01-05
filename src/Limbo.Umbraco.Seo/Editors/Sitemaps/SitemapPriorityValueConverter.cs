﻿using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

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

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {

            switch (source) {

                case float _:
                    return source;

                case string str:
                    return float.TryParse(str, out float result) ? result : default(float?);

                default:
                    return default(float?);

            }

        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return inter;
        }

    }

}