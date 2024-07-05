import { Component, OnInit, ViewChild } from '@angular/core';
import { EmpleadoService } from '../../Services/empleado.service';
import { Empleado } from '../../Models/empleado';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-empleado-list',
  templateUrl: './empleado-list.component.html',
  styleUrls: ['./empleado-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSnackBarModule
  ]
})

export class EmpleadoListComponent implements OnInit {
  listEmpleados: Empleado[] = [];
  displayedColumns: string[] = ['nombre', 'apellido', 'correo', 'telefono', 'puesto', 'accion'];
  dataSource!: MatTableDataSource<Empleado>;

  pageSize = 5;
  pageIndex = 0;
  length = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  filterValue = '';

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(private empleadoService: EmpleadoService, private router: Router, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<Empleado>();
    this.obtenerEmpleados();
  }

  obtenerEmpleados(): void {
    this.empleadoService.getEmpleados().subscribe({
      next: (data) => {
        this.listEmpleados = data;
        this.length = data.length;
        this.dataSource = new MatTableDataSource<Empleado>(this.listEmpleados);
        this.dataSource.paginator = this.paginator;
      },
      error: (err) => {
        console.error(err);
        this.showError('Error al obtener los empleados.');
      }
    });
  }

  addEmpleado(): void {
    this.router.navigate(['/empleado-form', 0]);
  }

  eliminar(empleado: Empleado): void {
    if (confirm(`¿Desea eliminar el empleado ${empleado.nombre} ${empleado.apellido}?`)) {
      this.empleadoService.eliminar(empleado.id).subscribe({
        next: () => this.obtenerEmpleados(),
        error: (err) => {
          console.error(err);
          this.showError('Error al eliminar el empleado.');
        }
      });
    }
  }

  pageChanged(event: any): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'Cerrar', {
      duration: 3000
    });
  }
}
