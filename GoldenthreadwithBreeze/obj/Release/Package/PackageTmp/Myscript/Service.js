//angular.service('GoldenThreadService').factory('dataservice', ['$http', '$q', '$timeout', 'breeze', 'logger', dataservice]);

app.service('GoldenThreadService', function ($http) {
$http.defaults.headers.common['AuthToken'] = '73DE4898-C1D3-42A4-B802-0B81F1E3867F';
//configureBreeze();
 var serviceName = "http://192.168.100.21:100" + '/api/' + 'Chapters/Get';
 ////var ms = new MetadataStore({hasServerMetadata: false});
 
  var manager = new breeze.EntityManager();
  manager.createEntity('Customer', {CompanyName:'Acme'});
 
  
  //model.initialize(manager.metadataStore);
  
    //Get All Chapter  
    this.getAllChapter = function () {	
		//	var query = breeze.EntityQuery			
		//		.from("Chapter")
		//		.where("Chapter.", "startsWith", "D")
		//		.orderBy('Title')
		//		.withParameters({
		//			$method: 'Get',
		//			$encoding: 'JSON',
		//			$headers: { 'AuthToken': '73DE4898-C1D3-42A4-B802-0B81F1E3867F' }
		//	});				
        //return manager.executeQuery(query)
        //    .then(getSucceeded)
        //    .fail(getFailed);
       
       var request = $http({
                    method: "Get",
                    url: "http://192.168.100.21:100/api/Chapters/Get",
                    headers: { 'AuthToken': '73DE4898-C1D3-42A4-B802-0B81F1E3867F' },
       });
       localStorage.setItem("Chapter", request);
      // manager.createEntity('Chapter', request);
	return request;			

    }

	function getSucceeded(data) { 

      return data.results;
  }


  function getFailed(error) {
      log("query failed: " + error.message);
      throw error; // so caller can hear it
  }
    //Create new record  
    this.post = function (Chapter) {
        var request = $http({
            method: "Post",
            url: "http://localhost:49426/api/Chapter/Add",
            headers: { 'AuthToken': '73DE4898-C1D3-42A4-B802-0B81F1E3867F' },
            data: JSON.stringify(Chapter)
        });
        return request;
    }

    //Delete the Record  
    this.delete = function (ID) {
        var request = $http({
            method: "delete",
            url: "http://localhost:49426/api/Chapter/Remove/" + ID,
            headers: { 'AuthToken': '73DE4898-C1D3-42A4-B802-0B81F1E3867F' },
        });
        return request;
    }

    //Get Single Records  
    this.get = function (ID) {
        var request = $http({
            method: "Get",
            url: "http://localhost:49426/api/Chapter/ChapterByID/" + ID,
            headers: { 'AuthToken': '73DE4898-C1D3-42A4-B802-0B81F1E3867F' },
        });
        return request;
    }

    //Update the Record  
    this.put = function (ID, Chapter) {
        var request = $http({
            method: "put",
            url: "http://localhost:49426/api/Chapter/Edit/" + ID,
            data: JSON.stringify(Chapter)
        });
        return request;
    }
function configureBreeze() {
    // configure to use the model library for Angular
    breeze.config.initializeAdapterInstance("modelLibrary", "backingStore", true);
    // configure to use camelCase
    breeze.NamingConvention.camelCase.setAsDefault();
  }
  
});