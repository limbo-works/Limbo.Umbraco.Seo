angular.module("umbraco").controller("Limbo.Seo.Preview", function ($scope, editorState) {

    $scope.title = "";
    $scope.description = "";

    $scope.editorState = editorState;

    let nameVariant = 0;

    let titleVariant = 0;
    let titleTab = -1;
    let titleProperty = -1;

    let seoTitleVariant = 0;
    let seoTitleTab = -1;
    let seoTitleProperty = -1;

    let teaserVariant = 0;
    let teaserTab = -1;
    let teaserProperty = -1;

    let seoMetaDescriptionVariant = 0;
    let seoMetaDescriptionTab = -1;
    let seoMetaDescriptionProperty = -1;

    const variant = $scope.editorState.current.variants[0];
    variant.tabs.forEach(function (tab, tabIndex) {

        tab.properties.forEach(function (property, propertyIndex) {

            switch (property.alias) {
                case "title":
                case "pageTitle":
                    titleTab = tabIndex;
                    titleProperty = propertyIndex;
                    break;
                case "seoTitle":
                    seoTitleTab = tabIndex;
                    seoTitleProperty = propertyIndex;
                    break;
                case "teaser":
                    teaserTab = tabIndex;
                    teaserProperty = propertyIndex;
                    break;
                case "seoMetaDescription":
                    seoMetaDescriptionTab = tabIndex;
                    seoMetaDescriptionProperty = propertyIndex;
                    break;
            }
        });
    });

    $scope.$watch(`editorState.current.variants[${nameVariant}].name`, function () {
        $scope.runTitle();
    });

    $scope.$watch(`editorState.current.variants[${titleVariant}].tabs[${titleTab}].properties[${titleProperty}].value`, function () {
        $scope.runTitle();
    });

    $scope.$watch(`editorState.current.variants[${seoTitleVariant}].tabs[${seoTitleTab}].properties[${seoTitleProperty}].value`, function () {
        $scope.runTitle();
    });

    $scope.$watch(`editorState.current.variants[${teaserVariant}].tabs[${teaserTab}].properties[${teaserProperty}].value`, function () {
        $scope.runDescription();
    });

    $scope.$watch(`editorState.current.variants[${seoMetaDescriptionVariant}].tabs[${seoMetaDescriptionTab}].properties[${seoMetaDescriptionProperty}].value`, function () {
        $scope.runDescription();
    });

    $scope.runTitle = function () {

        if (seoTitleTab >= 0 && seoTitleProperty >= 0) {
            if (editorState.current.variants[seoTitleVariant].tabs[seoTitleTab].properties[seoTitleProperty].value !== "") {
                $scope.title = editorState.current.variants[seoTitleVariant].tabs[seoTitleTab].properties[seoTitleProperty].value;
                return;
            }
        }

        if (titleTab >= 0 && titleProperty >= 0) {
            if (editorState.current.variants[titleVariant].tabs[titleTab].properties[titleProperty].value !== "") {
                $scope.title = editorState.current.variants[titleVariant].tabs[titleTab].properties[titleProperty].value;
                return;
            } 
        }

        $scope.title = editorState.current.variants[nameVariant].name;

    };

    $scope.runDescription = function () {

        if (seoMetaDescriptionTab >= 0 && seoMetaDescriptionProperty >= 0) {
            if (editorState.current.variants[seoMetaDescriptionVariant].tabs[seoMetaDescriptionTab].properties[seoMetaDescriptionProperty].value !== "") {
                $scope.description = editorState.current.variants[seoMetaDescriptionVariant].tabs[seoMetaDescriptionTab].properties[seoMetaDescriptionProperty].value;
                return;
            }
        }

        $scope.description = editorState.current.variants[teaserVariant].tabs[teaserTab].properties[teaserProperty].value;

    }

});