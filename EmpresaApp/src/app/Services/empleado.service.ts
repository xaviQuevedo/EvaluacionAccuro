import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Empleado } from '../Models/empleado';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {

  private apiUrl = 'http://localhost:5293/api/empleado';

  constructor(private http: HttpClient) { }

  // Método para manejar errores
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Error del lado del cliente
      errorMessage = `Ocurrio un error con el cliente: ${error.error.message}`;
    } else {
      // Error del lado del servidor
      errorMessage = `Código por el servidor: ${error.status}, mensaje de error: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }

  getEmpleados(): Observable<Empleado[]> {
    return this.http.get<Empleado[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  addEmpleado(empleado: Empleado): Observable<Empleado> {
    return this.http.post<Empleado>(this.apiUrl, empleado)
      .pipe(
        catchError(this.handleError)
      );
  }

  getEmpleadoById(id: number): Observable<Empleado> {
    return this.http.get<Empleado>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  eliminar(idEmpleado: number): Observable<void> {
    const url = `${this.apiUrl}/${idEmpleado}`;
    return this.http.delete<void>(url)
      .pipe(
        catchError(this.handleError)
      );
  }
}
