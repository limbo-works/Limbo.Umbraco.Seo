angular.module("umbraco").controller("Limbo.Umbraco.Seo.SitemapPriority", function ($scope) {

    const vm = this;

    vm.priorities = [
        { value: "0.0" },
        { value: "0.1" },
        { value: "0.2" },
        { value: "0.3" },
        { value: "0.4" },
        { value: "0.5" },
        { value: "0.6" },
        { value: "0.7" },
        { value: "0.8" },
        { value: "0.9" },
        { value: "1.0" }
    ];

    $scope.model.value = $scope.model.value ? $scope.model.value : "0.5";

    vm.priorities.forEach(function(p) {
        p.selected = p.value === $scope.model.value;
    });

    vm.select = function(priority) {
        $scope.model.value = priority.value;
        vm.priorities.forEach(function (p) {
            p.selected = p === priority;
        });
    };

});