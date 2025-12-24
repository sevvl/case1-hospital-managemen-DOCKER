import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../../core/services/patient.service';
import { AiService } from '../../../core/services/ai.service';
import { PatientDetail } from '../../../core/models/patient.model';
import { AiPrediction } from '../../../core/models/prediction.model';

@Component({
  selector: 'app-patient-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.css']
})
export class PatientDetailComponent implements OnInit {
  patient: PatientDetail | null = null;
  prediction: AiPrediction | null = null;
  loading = false;
  error = '';
  patientId: number = 0;

  constructor(
    private patientService: PatientService,
    private aiService: AiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      this.patientId = params['id'];
      this.loadPatient();
      this.loadPrediction();
    });
  }

  loadPatient(): void {
    this.loading = true;
    this.error = '';
    this.patientService.getById(this.patientId).subscribe({
      next: (data) => {
        this.patient = data;
        this.loading = false;
      },
      error: (error: any) => {
        this.error = 'Hasta bilgileri yüklenirken hata oluştu.';
        this.loading = false;
      }
    });
  }

  loadPrediction(): void {
    this.aiService.getPrediction(this.patientId).subscribe({
      next: (data) => {
        this.prediction = data;
      },
      error: (error: any) => {
        // AI tahminini alamazsa sessiz kalan hata
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/patients']);
  }
}
