function SearchController($scope) {
	$scope.isLoadingSolr = false;
	$scope.solrResult = null;
	$scope.categoryFilter = '';

	$scope.search = function () {
		$scope.isLoadingSolr = true;

		$.getJSON(
			'/api/solrsearch/' + $scope.categoryFilter,
			{ 'q': $scope.searchTerm },
			function (data) {
				$scope.$apply(function () {
					$scope.isLoadingSolr = false;
					$scope.solrResult = data;
				});
			}
		);
	};
}