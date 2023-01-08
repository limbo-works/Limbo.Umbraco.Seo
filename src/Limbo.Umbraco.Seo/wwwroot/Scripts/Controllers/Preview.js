angular.module("umbraco").controller("Limbo.Umbraco.Seo.Preview", function ($scope, editorState) {

    const vm = this;

    vm.title = "";
    vm.description = "";

    $scope.editorState = editorState;

    vm.titleProperties = [];
    vm.descriptionProperties = [];

    vm.properties = {
        title: [],
        description: []
    };

    if ($scope.model.config) {

        if (typeof $scope.model.config.title === "string") {
            vm.titleProperties = $scope.model.config.title
                .split(",")
                .map(x => x.trim())
                .filter(x => !!x);
        }

        if (typeof $scope.model.config.description === "string") {
            vm.descriptionProperties = $scope.model.config.description
                .split(",")
                .map(x => x.trim())
                .filter(x => !!x);
        }

    }

    // Set fallback values if not specified on the data type
    if (vm.titleProperties.length === 0) vm.titleProperties = ["seoTitle", "title"];
    if (vm.descriptionProperties.length === 0) vm.descriptionProperties = ["seoMetaDescription", "teaser", "introTeaser"];

    // TODO: Add support for variants
    const variantIndex = 0;
    const variant = $scope.editorState.current.variants[variantIndex];

    // Listen for changes on the "name" field
    const nameVariant = 0;
    $scope.$watch(`editorState.current.variants[${nameVariant}].name`, function () {
        vm.titleUpdated();
    });

    vm.titleProperties.forEach(function (alias) {

        let hasProperty = false;

        variant.tabs.forEach(function (tab, tabIndex) {

            tab.properties.forEach(function (property, propertyIndex) {

                if (hasProperty) return;
                if (property.alias !== alias) return;

                hasProperty = true;

                vm.properties.title.push(property);

                $scope.$watch(`editorState.current.variants[${variantIndex}].tabs[${tabIndex}].properties[${propertyIndex}].value`, function () {
                    vm.titleUpdated();
                });

            });

        });

    });

    vm.descriptionProperties.forEach(function (alias) {

        let hasProperty = false;

        variant.tabs.forEach(function (tab, tabIndex) {

            tab.properties.forEach(function (property, propertyIndex) {

                if (hasProperty) return;
                if (property.alias !== alias) return;

                hasProperty = true;

                vm.properties.description.push(property);

                $scope.$watch(`editorState.current.variants[${variantIndex}].tabs[${tabIndex}].properties[${propertyIndex}].value`, function () {
                    vm.descriptionUpdated();
                });

            });

        });

    });

    vm.titleUpdated = function () {

        for (let i = 0; i < vm.properties.title.length; i++) {

            if (!vm.properties.title[i].value) continue;

            vm.title = vm.properties.title[i].value;
            return;

        }

        vm.title = editorState.current.variants[nameVariant].name;

    };

    vm.descriptionUpdated = function () {

        for (let i = 0; i < vm.properties.description.length; i++) {

            if (!vm.properties.description[i].value) continue;

            vm.description = vm.properties.description[i].value;
            return;

        }

        vm.description = "";

    };

    vm.titleUpdated();
    vm.descriptionUpdated();

});