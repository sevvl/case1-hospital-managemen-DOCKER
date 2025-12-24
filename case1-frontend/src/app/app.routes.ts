import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { PatientListComponent } from './features/patients/patient-list/patient-list.component';
import { PatientDetailComponent } from './features/patients/patient-detail/patient-detail.component';
import { PatientCreateComponent } from './features/patients/patient-create/patient-create.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { 
    path: 'patients', 
    component: PatientListComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'patients/create', 
    component: PatientCreateComponent,
    canActivate: [authGuard]
  },
  { 
    path: 'patients/:id', 
    component: PatientDetailComponent,
    canActivate: [authGuard]
  },
  { path: '', redirectTo: '/patients', pathMatch: 'full' },
  { path: '**', redirectTo: '/patients' }
];

