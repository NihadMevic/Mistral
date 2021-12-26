import { Component, OnInit,Input, Output, EventEmitter  } from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  @Input() TitleLabel: any;
  @Output() tabSwitch: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  switch(tab: any) {
    this.tabSwitch.emit(tab);
  }

}
