import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { OpcuaFormComponent } from './opcua-form/opcua-form.component';
import { CommonModule } from '@angular/common';
import { NodeData } from './node/node.component';
import { SubmodelData } from './models/submodel-data.model';
import { SubmodelListComponent } from './submodel-list/submodel-list.component';
import { Workbench } from './models/workbenchData';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule, RouterOutlet, OpcuaFormComponent, CommonModule, SubmodelListComponent, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'opcua-app';
  droppedSubmodelData: SubmodelData[] = [];
  workbenchName: string = '';
  workbenches: Workbench[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadWorkbenches();
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDropToWorkbench(event: DragEvent) {
    event.preventDefault();
    const data = event.dataTransfer?.getData('application/json');
    const type = event.dataTransfer?.getData('type');

    console.log('Drop data:', data);

    if (data && type === 'submodel') {
      const submodel = JSON.parse(data);
      const submodelId = submodel.submodels[0].keys[0].value;
      this.fetchSubmodelData(btoa(submodelId));
    }
  }

  fetchSubmodelData(submodelId: string) {
    this.http.get<SubmodelData>(`http://localhost:5000/api/aas/submodels/${submodelId}`).subscribe(
      data => {
        this.droppedSubmodelData.push(data);
      },
      error => {
        console.error('Failed to fetch submodel data', error);
      }
    );
  }

  onDropToSubmodel(event: DragEvent, submodelId: string) {
    event.preventDefault();
    const data = event.dataTransfer?.getData('application/json');
    const type = event.dataTransfer?.getData('type');

    if (data && type === 'node') {
      const node = JSON.parse(data) as NodeData;
      console.log(node);
      this.updateSubmodel(btoa(submodelId), node);
    }
  }

  updateSubmodel(submodelId: string, node: NodeData) {
    this.http.get<SubmodelData>(`http://localhost:5000/api/aas/submodels/${submodelId}`).subscribe(
      submodelData => {
        const updatedSubmodel = this.constructUpdatedSubmodel(submodelData, node);
        console.log(submodelData);
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        this.http.put(`http://localhost:8081/submodels/${submodelId}`, updatedSubmodel, { headers }).subscribe(
          () => {
            console.log('Submodel updated successfully');
            this.updateDisplayedSubmodel(submodelId);
          },
          error => {
            console.error('Failed to update submodel', error);
          }
        );
      },
      error => {
        console.error('Failed to fetch submodel data for update', error);
      }
    );
  }

  updateDisplayedSubmodel(submodelId: string) {
    this.http.get<SubmodelData>(`http://localhost:5000/api/aas/submodels/${submodelId}`).subscribe(
      updatedData => {
        this.droppedSubmodelData = this.droppedSubmodelData.map(submodel => {
          if (btoa(submodel.id) === submodelId) {
            return updatedData;
          }
          return submodel;
        });
      },
      error => {
        console.error('Failed to fetch updated submodel data', error);
      }
    );
  }  

  constructUpdatedSubmodel(submodelData: SubmodelData, node: NodeData): SubmodelData {
    const updatedSubmodel = { ...submodelData };
  
    updatedSubmodel.submodelElements.forEach(element => {
      if (element.idShort === 'INPUTS' || element.idShort === 'OUTPUTS' || element.idShort === 'STATES') {
        element.value.forEach(subElement => {
          if (element.idShort === 'INPUTS' || element.idShort === 'OUTPUTS') {
            subElement.value.forEach(property => {
              if (property.idShort === 'NodeID') {
                property.value = node.nodeId;
              }
            });
          }
          if (element.idShort === 'STATES') {
            subElement.value.forEach(property => {
              if (property.idShort === 'NodeName') {
                property.value = node.displayName;
              }
            });
          }
        });
      }
    });
  
    return updatedSubmodel;
  }

  openSaveDialog() {
    const saveDialog = document.getElementById('saveDialog') as HTMLDialogElement;
    if (saveDialog) {
      saveDialog.showModal();
    }
  }

  closeSaveDialog() {
    const saveDialog = document.getElementById('saveDialog') as HTMLDialogElement;
    if (saveDialog) {
      saveDialog.close();
    }
  }

  saveWorkbench() {
    const workbench: Workbench = {
      id: '',
      name: this.workbenchName,
      submodelData: this.droppedSubmodelData
    };
    this.http.post('http://localhost:5000/api/workbench', workbench).subscribe(
      () => {
        console.log('Workbench saved successfully');
        this.loadWorkbenches();
        this.closeSaveDialog();
      },
      error => {
        console.error('Failed to save workbench', error);
      }
    );
  }

  loadWorkbenches() {
    this.http.get<Workbench[]>('http://localhost:5000/api/workbench').subscribe(
      data => {
        this.workbenches = data;
      },
      error => {
        console.error('Failed to load workbenches', error);
      }
    );
  }

  loadWorkbench(id: string) {
    this.droppedSubmodelData = []
    console.log(id);
    
    this.http.get<Workbench>(`http://localhost:5000/api/workbench/${id}`).subscribe(
      data => {
        this.droppedSubmodelData = data.submodelData;
      },
      error => {
        console.error('Failed to load workbench', error);
      }
    );
  }

  deleteWorkbench(id: string) {
    this.http.delete(`http://localhost:5000/api/workbench/${id}`).subscribe(
      () => {
        console.log('Workbench deleted successfully');
        this.workbenches = this.workbenches.filter(w => w.id !== id);
      },
      error => {
        console.error('Failed to delete workbench', error);
      }
    );
  }
}