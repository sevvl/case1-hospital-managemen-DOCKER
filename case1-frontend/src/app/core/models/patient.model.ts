export interface Patient {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
}

export interface PatientDetail extends Patient {
  records: MedicalRecord[];
}

export interface MedicalRecord {
  id: number;
  date: Date;
  doctorRemarks: string;
  diagnosis: string;
}

export interface CreatePatientRequest {
  name: string;
  surname: string;
  birthDate: Date;
}
