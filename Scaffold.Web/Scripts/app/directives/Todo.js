﻿/*global app */
'use strict';

/**
 * Directive that executes an expression when the element it is applied to loses focus
 */
app.directive('todoBlur', function () {
	return function (scope, elem, attrs) {
		elem.bind('blur', function () {
			scope.$apply(attrs.todoBlur);
		});
	};
});

/**
 * Directive that executes an expression when the element it is applied to gets
 * an `escape` keydown event.
 */
app.directive('todoBlur', function () {
	var ESCAPE_KEY = 27;
	return function (scope, elem, attrs) {
		elem.bind('keydown', function (event) {
			if (event.keyCode === ESCAPE_KEY) {
				scope.$apply(attrs.todoEscape);
			}
		});
	};
});

/**
 * Directive that places focus on the element it is applied to when the expression it binds to evaluates to true
 */
app.directive('todoFocus', function todoFocus($timeout) {
	return function (scope, elem, attrs) {
		scope.$watch(attrs.todoFocus, function (newVal) {
			if (newVal) {
				$timeout(function () {
					elem[0].focus();
				}, 0, false);
			}
		});
	};
});