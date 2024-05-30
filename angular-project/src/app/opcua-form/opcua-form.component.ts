import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NodeComponent, NodeData } from '../node/node.component';

@Component({
  standalone: true,
  selector: 'app-opcua-form',
  templateUrl: './opcua-form.component.html',
  styleUrl: './opcua-form.component.css',
  imports: [NodeComponent, CommonModule, FormsModule]
})
export class OpcuaFormComponent {
  endpoint: string = '';
  message: string = '';
  nodes: NodeData[] = [];

  @Output() endpointChange = new EventEmitter<string>();

  constructor(private http: HttpClient) {}

  connectToServer() {
    const connectUrl = `http://localhost:5000/api/opcua/connect`;
    this.http.post(connectUrl, { endpoint: this.endpoint }).subscribe(
      (response: any) => {
        this.message = response.message;
        this.endpointChange.emit(this.endpoint);
        this.browseServer();
      },
      (error) => {
        this.message = 'Error connecting to server';
      }
    );
  }

  browseServer() {
    const browseUrl = `http://localhost:5000/api/opcua/browse?endpoint=${encodeURIComponent(this.endpoint)}`;
    this.http.get<NodeData[]>(browseUrl).subscribe(
      (data) => {
        this.nodes = data;
      },
      (error) => {
        this.message = 'Error browsing server';
      }
    );
  }

  onNodeToggle(node: NodeData) {
    if (!node.children || node.children.length === 0) {
        const browseUrl = `http://localhost:5000/api/opcua/browse?endpoint=${encodeURIComponent(this.endpoint)}&nodeId=${encodeURIComponent(node.nodeId)}`;
        this.http.get<NodeData[]>(browseUrl).subscribe(
            (data) => {
              node.children = data.length > 0 ? data : undefined; 
              node.expanded = data.length > 0;
            },
            (error) => {
              this.message = 'Error loading child nodes';
            }
        );
    } else {
        node.expanded = !node.expanded;
    }
  }
}