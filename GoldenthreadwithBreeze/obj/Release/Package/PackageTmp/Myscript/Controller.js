app.controller('GoldenThreadController',  function ($scope, GoldenThreadService) {
    $scope.OperType = 1;
	
    //1 Mean New Entry  
    GetAllRecords();

    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = GoldenThreadService.getAllChapter();
        promiseGet.then(function (pl) {
		$scope.Chapters = pl.results
		},
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);
              });
    }

    //To Clear all input controls.  
    function ClearModels() {
        $scope.OperType = 1;
        $scope.ID = "";
        $scope.IDApp = "";
        $scope.Title = "";
        $scope.CreatedBy = "";
        $scope.ModifiedBy = "";
    }

    //To Create new record and Edit an existing Record.  
    $scope.save = function () {
        if ($scope.OperType === 1) {
            var Chapter = {
                ID: null,
                IDApp:null,
                Title: $scope.Title,
                CreatedBy: $scope.CreatedBy,
                ModifiedBy: $scope.ModifiedBy,
            };
                var promisePost = GoldenThreadService.post(Chapter);
                promisePost.then(function (pl) {
                    GetAllRecords();
                    ClearModels();
                }, function (err) {
                    console.log("Err" + err);
                });
            }
            else {
            var Chapter = {
                IDApp: $scope.IDApp,
                Title: $scope.Title,
                ModifiedBy: $scope.ModifiedBy,
            };
                //Edit the record 
            Chapter.ID = $scope.ID;
                var promisePut = GoldenThreadService.put($scope.ID, Chapter);
                promisePut.then(function (pl) {
                    $scope.Message = "Student Updated Successfuly";
                    GetAllRecords();
                    ClearModels();
                }, function (err) {
                    console.log("Err" + err);
                });
            }
    };


    //To Get Student Detail on the Base of Student ID  
    $scope.get = function (Chapter) {
        var promiseGetSingle = GoldenThreadService.get(Chapter.ID);
        promiseGetSingle.then(function (pl) {
            var res = pl.data;
            $scope.ID = res.ID;
            $scope.IDApp = res.IDApp;
            $scope.Title = res.Title;
            $scope.CreatedBy = res.CreatedBy;
            $scope.ModifiedBy = res.ModifiedBy;
            $scope.OperType = 0;
        },
                  function (errorPl) {
                      console.log('Some Error in Getting Details', errorPl);
                  });
    }

    //To Delete Record  
    $scope.delete = function (Chapter) {
        var promiseDelete = GoldenThreadService.delete(Chapter.ID);
        promiseDelete.then(function (pl) {
            $scope.Message = "Student Deleted Successfuly";
            GetAllRecords();
            ClearModels();
        }, function (err) {
            console.log("Err" + err);
        });
    }

    //To Clear all Inputs controls value.  
    $scope.clear = function () {
        $scope.OperType = 1;
        $scope.ID = "";
        $scope.IDApp = "";
        $scope.Title = "";
        $scope.CreatedBy = "";
        $scope.ModifiedBy = "";
    }

});
