import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AasData } from '../models/aas-data.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-submodel-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './submodel-list.component.html',
  styleUrl: './submodel-list.component.css'
})
export class SubmodelListComponent implements OnInit {
  submodels: AasData[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<{ result: AasData[] }>('http://localhost:5000/api/aas/shells').subscribe(
      response => {
        this.submodels = response.result;
      },
      error => {
        console.error('Failed to fetch submodels', error);
      }
    );
  }

  onDragStart(event: DragEvent, aas: AasData): void {
    event.stopPropagation();
    event.dataTransfer?.setData('type', 'submodel');
    event.dataTransfer?.setData('application/json', JSON.stringify(aas));
  }
}