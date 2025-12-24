import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../../core/services/patient.service';
import { CreatePatientRequest } from '../../../core/models/patient.model';

@Component({
  selector: 'app-patient-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patient-create.component.html',
  styleUrls: ['./patient-create.component.css']
})
export class PatientCreateComponent {
  patientForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private router: Router
  ) {
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      birthDate: ['', Validators.required]
    });
  }

  get f() {
    return this.patientForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.error = '';

    if (this.patientForm.invalid) {
      return;
    }

    this.loading = true;
    const request: CreatePatientRequest = {
      name: this.patientForm.value.name,
      surname: this.patientForm.value.surname,
      birthDate: new Date(this.patientForm.value.birthDate)
    };

    this.patientService.create(request).subscribe({
      next: (patient) => {
        this.router.navigate(['/patients', patient.id]);
      },
      error: (error: any) => {
        this.error = 'Hasta eklenirken hata oluştu.';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/patients']);
  }
}
