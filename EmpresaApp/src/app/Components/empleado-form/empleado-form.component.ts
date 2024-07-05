import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EmpleadoService } from '../../Services/empleado.service';
import { Empleado } from '../../Models/empleado';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-empleado-form',
  templateUrl: './empleado-form.component.html',
  styleUrls: ['./empleado-form.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule
  ]
})
export class EmpleadoFormComponent implements OnInit {
  empleadoForm: FormGroup;
  empleadoId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private empleadoService: EmpleadoService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {
    this.empleadoForm = this.fb.group({
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      correo: ['', [Validators.required, Validators.email]],
      telefono: ['', [Validators.required, Validators.pattern('[0-9]{9}')]],
      puesto: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.empleadoId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.empleadoId) {
      this.empleadoService.getEmpleadoById(this.empleadoId).subscribe({
        next: (empleado) => this.empleadoForm.patchValue(empleado),
        error: (err) => {
          console.error(err);
          this.showError('Error al obtener los datos del empleado.');
        }
      });
    }
  }

  onSubmit(): void {
    if (this.empleadoForm.valid) {
      const empleado: Empleado = this.empleadoForm.value;
      if (this.empleadoId) {
        empleado.id = this.empleadoId;
      } else {
        this.empleadoService.addEmpleado(empleado).subscribe({
          next: () => this.router.navigate(['/empleados']),
          error: (err) => {
            console.error(err);
            this.showError('Error al agregar el empleado.');
          }
        });
      }
    }
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'Cerrar', {
      duration: 3000
    });
  }
}
