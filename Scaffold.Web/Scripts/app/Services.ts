// WARNING: T4 Template generated Javascript proxy client objects (it depends on CodeToJSonData)
//
/// <reference path="../typings/jquery/jquery.d.ts"/>
/// <reference path="Models.ts"/>
module App.Services {

    export class TodoService {
        cache = false;
        timeout = 2000;
		async = true;
        constructor(public baseurl: string) {}

        GetAll(): JQueryPromise<Array<App.Models.ITodo>> {
		    var res = $.ajax({
			    cache: this.cache,
			    async: this.async,
			    timeout: this.timeout,
			    dataType: 'json',
				contentType: 'application/json',
			    type: 'GET',
			    url: this.baseurl + 'api/Todo/GetAll',
		    })
			;
            return res;
        }

        Get(id: number): JQueryPromise<App.Models.ITodo> {
		    var res = $.ajax({
			    cache: this.cache,
			    async: this.async,
			    timeout: this.timeout,
			    dataType: 'json',
				contentType: 'application/json',
			    type: 'GET',
			    url: this.baseurl + 'api/Todo/Get?id='+id+'',
		    })
			;
            return res;
        }
    }
}
