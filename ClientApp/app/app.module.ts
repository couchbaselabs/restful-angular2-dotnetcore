import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { FormsModule }   from '@angular/forms';
import { ItemComponent } from './components/item/item.component';
import { ListComponent } from './components/list/list.component';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        ItemComponent,
        ListComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: "", component: ListComponent },
            { path: "item", component: ItemComponent },
            { path: "item/:documentId", component: ItemComponent },
        ])
    ]
})
export class AppModule {
}
