import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../../core/services/patient.service';
import { Patient } from '../../../core/models/patient.model';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  patients: Patient[] = [];
  loading = false;
  error = '';

  constructor(
    private patientService: PatientService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.loading = true;
    this.error = '';
    this.patientService.getAll().subscribe({
      next: (data) => {
        this.patients = data;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = 'Hastalar yüklenirken hata oluştu.';
        this.loading = false;
      }
    });
  }

  viewDetails(id: number): void {
    this.router.navigate(['/patients', id]);
  }

  createNew(): void {
    this.router.navigate(['/patients/create']);
  }

  deletePatient(id: number): void {
    if (confirm('Hastayı silmek istediğinize emin misiniz?')) {
      this.patientService.delete(id).subscribe({
        next: () => {
          this.patients = this.patients.filter(p => p.id !== id);
        },
        error: (error: any) => {
          this.error = 'Hasta silinirken hata oluştu.';
        }
      });
    }
  }
}
