function SearchController($scope) {
    $scope.isLoadingSolr = false;

    $scope.solrResults = [];
    
    $scope.search = function () {
        $scope.isLoadingSolr = true;

        $.getJSON(
            '/api/solrsearch/' + $scope.searchTerm, function (data) {
                $scope.$apply(function () {
                    $scope.isLoadingSolr = false;
                    $scope.solrResults = data;
                });
            }
        );
    };
}