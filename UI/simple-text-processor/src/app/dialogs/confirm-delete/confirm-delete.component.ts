import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-confirm-delete',
  templateUrl: './confirm-delete.component.html',
  styleUrls: ['./confirm-delete.component.scss']
})
export class ConfirmDeleteComponent implements OnInit {
  @Input() public title!: string;
  @Input() public confirmText!: string;
  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  public close = () => {
    this.activeModal.close("OK");
  };

  public dismiss() {
    this.activeModal.dismiss("Cancel");
  }
}
