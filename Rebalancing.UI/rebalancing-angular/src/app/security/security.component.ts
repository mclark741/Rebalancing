import { Component, OnInit, Input } from '@angular/core';
import { Security } from '../security';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.scss'],
})
export class SecurityComponent implements OnInit {
  @Input() security?: Security;
  constructor() {}

  ngOnInit(): void {}
}
