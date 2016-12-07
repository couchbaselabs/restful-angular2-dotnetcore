import { Component, OnInit } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'list',
    templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {

    public people: Array<any>;

    public constructor(private http: Http) {
        this.people = [];
    }

    public ngOnInit() {
        console.log("boo");
        this.http.get("/api/getAll")
            .subscribe(results => {
                this.people = results.json();
            }, error => {
                console.error(error);
            });
    }

    public delete(documentId: string) {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let options = new RequestOptions({ headers: headers });
        this.http.delete("/api/delete/" + documentId) //, options)
            .subscribe(results => {
                for(let i = 0; i < this.people.length; i++) {
                    if(this.people[i].id == documentId) {
                        this.people.splice(i, 1);
                        break;
                    }
                }
            }, error => {
                console.error(error);
            });
    }

}
