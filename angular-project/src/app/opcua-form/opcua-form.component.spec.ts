import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcuaFormComponent } from './opcua-form.component';

describe('OpcuaFormComponent', () => {
  let component: OpcuaFormComponent;
  let fixture: ComponentFixture<OpcuaFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OpcuaFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OpcuaFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
