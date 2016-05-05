var ShapeApp = angular.module("ShapeApp",[]);

ShapeApp.controller("shapesParentController",function($scope){
	$scope.count = 0;
});

ShapeApp.controller("circleController", function($scope){
	$scope.count = 0;
	$scope.disabled = false;
	$scope.circles = [];
	$scope.circle = {radius: 0,area: 0};
	$scope.Add = function(){
		$scope.circle.area = Math.PI*($scope.circle.radius*$scope.circle.radius);
		$scope.circles.push(angular.copy($scope.circle));
		if($scope.circles.length >=10)
		{
			$scope.disabled = true;
		}
		$scope.count++;
		$scope.$parent.count++;
	}
	
});

ShapeApp.controller("squareController", function($scope){
	$scope.count = 0;
	$scope.disabled = false;
	$scope.squares = [];
	$scope.square = {side: 0,area: 0};
	$scope.Add = function(){
		$scope.square.area = $scope.square.side*$scope.square.side;
		$scope.squares.push(angular.copy($scope.square));
		if($scope.squares.length >=10)
		{
			$scope.disabled = true;
		}
		$scope.count++;
		$scope.$parent.count++;
	}
	
});

ShapeApp.controller("rectangleController", function($scope){
	$scope.count = 0;
	$scope.disabled = false;
	$scope.rectangles = [];
	$scope.rectangle = {width: 0,height: 0,area: 0};
	$scope.Add = function(){
		$scope.rectangle.area = $scope.rectangle.width*$scope.rectangle.height;
		$scope.rectangles.push(angular.copy($scope.rectangle));
		if($scope.rectangles.length >=10)
		{
			$scope.disabled = true;
		}
		$scope.count++;
		$scope.$parent.count++;
	}
	
});

ShapeApp.controller("triangleController", function($scope){
	$scope.count = 0;
	$scope.disabled = false;
	$scope.triangles = [];
	$scope.Add = function(){
		$scope.triangle.area = ($scope.triangle.width*$scope.triangle.height)/2;
		$scope.triangles.push(angular.copy($scope.triangle));
		if($scope.triangles.length >=10)
		{
			$scope.disabled = true;
		}
		$scope.count++;
		$scope.$parent.count++;
	}
	
});