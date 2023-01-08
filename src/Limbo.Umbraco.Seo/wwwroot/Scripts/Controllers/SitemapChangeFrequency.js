angular.module("umbraco").controller("Limbo.Umbraco.Seo.SitemapChangeFrequency", function ($scope, localizationService) {

    const vm = this;

    vm.frequencies = [
        { value: "", name: "Unspecified" },
        { value: "always", name: "Always" },
        { value: "hourly", name: "Hourly" },
        { value: "daily", name: "Daily" },
        { value: "weekly", name: "Weekly" },
        { value: "monthly", name: "Monthly" },
        { value: "yearly", name: "Yearly" },
        { value: "never", name: "Never" }
    ];

    $scope.model.value = $scope.model.value ? $scope.model.value : "";

    vm.frequencies.forEach(function (f) {
        f.selected = f.value === $scope.model.value;

        localizationService.localize(`limboSeo_frequency_${f.value === "" ? "unspecified" : f.value}`).then(function (value) {
            if (typeof value === "string" && value.length > 0 && value[0] !== "[") f.name = value;
        });

    });

    vm.select = function(frequency) {
        $scope.model.value = frequency.value;
        vm.frequencies.forEach(function (f) {
            f.selected = f === frequency;
        });
    };

});