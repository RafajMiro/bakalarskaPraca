import { Routes } from '@angular/router';
import { OpcuaFormComponent } from './opcua-form/opcua-form.component';
import { NodeComponent } from './node/node.component';
import { SubmodelListComponent } from './submodel-list/submodel-list.component';

export const routes: Routes = [
    { path: 'opcua-form', component: OpcuaFormComponent },
    { path: 'node', component: NodeComponent},
    { path: 'submodel-list', component: SubmodelListComponent},

];