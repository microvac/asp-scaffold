// WARNING: T4 generated file  (it is related to CodeToServerProxy)
//

/// <reference path="../typings/jquery/jquery.d.ts"/>

module Scaffold {
    export class AjaxSettings {
        async = true;
        cache = false;
        timeout = 2000;

        public build(settings: JQueryAjaxSettings): JQueryAjaxSettings{
            return {
                async: this.async,
                cache: this.cache,
                timeout: this.timeout,
                dataType: 'json',
				contentType: 'application/json',
			    type: settings.type,
			    url: settings.url,
                data: settings.data
            }
        }

    }
}

module App.Models {
    // C:\Workspace\vstudio\Scaffold\Scaffold.Web\Models\Todo.cs: 10
    export interface ITodo {
        ID: number;
        Name: string;
        Description: string;
    }

    export class Todo {
        public static ajaxSettings = new Scaffold.AjaxSettings();
        ID: number;
        Name: string;
        Description: string;
        constructor(data?: ITodo) {
            this.ID = data ? data.ID : null;
            this.Name = data ? data.Name : null;
            this.Description = data ? data.Description : null;
        }

        /* App.Controllers.TodoController */

        static GetAll(): JQueryPromise<Array<Todo>> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/GetAll',
            })).then((models) => {
                return models.map((model) => new Todo(model));
            });
            return res;
        }

        static Get(id: number): JQueryPromise<Todo> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/Get/'+id,
            })).then((model) => new Todo(model));
            return res;
        }
                
        Save(): JQueryPromise<void> {
            var isNew = this.ID == null;
            var model = this;
            var res = $.ajax(Todo.ajaxSettings.build({
                type: isNew ? 'POST' : 'PUT',
                url: '/api/Todo/'+(isNew ? 'Post' : 'Put'),
                data: JSON.stringify(this)
            })).then((id) => {
                if(isNew){
                    this.ID = id;
                }
            });
            return res;
        }

        Delete(): JQueryPromise<void> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'DELETE',
                url: '/api/Todo/Delete/'+this.ID,
            }));
            return res;
        }

        static Delete(id: number): JQueryPromise<void> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/Delete/'+id,
            }));
            return res;
        }
                
        GetAduh(): JQueryPromise<number> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/GetAduh',
            }));
            return res;
        }
    }

}
