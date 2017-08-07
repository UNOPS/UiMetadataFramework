import { NgModule } from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';

import { FormsListComponent } from './app/forms-list/forms-list-component';
import { FormViewComponent } from './app/form-view/form-view.component';
import { FormResolver } from "./form.resolver";
//import { FormResolver } from './form.resolver';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '',
        pathMatch:'full'
    },
    {
        path: 'forms',
        component: FormsListComponent
    },
    {
        path: 'form/:id',
        component: FormViewComponent,
        resolve : {'form11' : FormResolver}
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes, { useHash: true })
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {}