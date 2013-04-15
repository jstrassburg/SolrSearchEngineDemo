function SearchController($scope) {
	$scope.isLoadingSolr = false;
	$scope.solrResult = null;
	$scope.categoryFilter = '';
	$scope.lowPrice = '';
	$scope.highPrice = '';

	$scope.search = function () {
		$scope.isLoadingSolr = true;

		$.getJSON(
			'/api/solrsearch/' + $scope.categoryFilter,
			{
				'q': $scope.searchTerm,
				'lowPrice': $scope.lowPrice,
				'highPrice': $scope.highPrice
			},
			function (data) {
				$scope.$apply(function () {
					$scope.isLoadingSolr = false;
					$scope.solrResult = data;
				});
			}
		);
		$scope.categoryFilter = '';
		$scope.lowPrice = '';
		$scope.highPrice = '';
	};

	$scope.filterCategory = function(category) {
		$scope.categoryFilter = category;
		$scope.search();
	};

	$scope.filterPriceRange = function(lowPrice, highPrice) {
		$scope.lowPrice = lowPrice;
		$scope.highPrice = highPrice;
		$scope.search();
	};
}