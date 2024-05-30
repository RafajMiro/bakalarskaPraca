import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

export interface NodeData {
  nodeId: string;
  displayName: string;
  nodeClass: string;
  expanded?: boolean;
  children?: NodeData[];
}

@Component({
  selector: 'app-node',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './node.component.html',
  styleUrl: './node.component.css'
})
export class NodeComponent {
  @Input() node!: NodeData;
  @Output() toggle = new EventEmitter<NodeData>();

  expand(event: MouseEvent) {
    event.stopPropagation();
    if (this.node.children && this.node.children.length > 0) {
      this.node.expanded = !this.node.expanded;
    } else {
      this.toggle.emit(this.node);
    }
  }

  onDragStart(event: DragEvent) {
    event.stopPropagation();
    if (this.node.nodeClass == "Variable"){
      event.dataTransfer?.setData('type', 'node');
      event.dataTransfer?.setData('application/json', JSON.stringify(this.node));
    }
  }
}