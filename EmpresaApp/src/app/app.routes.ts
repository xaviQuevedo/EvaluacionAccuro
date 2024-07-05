import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmpleadoListComponent } from './Components/empleado-list/empleado-list.component';
import { EmpleadoFormComponent } from './Components/empleado-form/empleado-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/empleados', pathMatch: 'full' },
  { path: 'empleados', component: EmpleadoListComponent },
  { path: 'empleado-form/:id', component: EmpleadoFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


/*
import { Routes } from '@angular/router';
import { EmpleadoListComponent } from './Components/empleado-list/empleado-list.component';
import { EmpleadoFormComponent } from './Components/empleado-form/empleado-form.component';

export const routes: Routes = [
    {path: '',component: EmpleadoListComponent},
    {path: 'empleado-list',component: EmpleadoListComponent},
    {path: 'empleado-form/:id',component: EmpleadoFormComponent}

];
*/