import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import { FileModel } from '../file.model'
import { TextService } from '../text.service'
import { ConfirmDeleteComponent } from 'src/app/dialogs/confirm-delete/confirm-delete.component';
import { NotificationService } from '../../notification/notification.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  public files: FileModel[] = [];
  constructor(private _textService: TextService,
    private _notificationService: NotificationService,
    private _router: Router,
    private _modalService: NgbModal) { }

  ngOnInit(): void {
    this.getFiles();
  }

  public getFiles = () => {
    this._textService.getFiles()
      .subscribe({
        next: (res: any) => {
          this.files = res as FileModel[];
        },
        error: (err: any) => {
          // Handle an error
          console.error(err);
        }
      });
  }

  public redirectToAdd = () => {
    const url: string = 'add';
    this._router.navigateByUrl(url);
  }

  public redirectToUpdate = (file: FileModel) => {
    const url: string = `update/${file.name}/size/${file.size}`;
    this._router.navigateByUrl(url);
  }

  public openDeleteConfirmDialog = (fileName: string) => {
    const modalRef = this._modalService.open(ConfirmDeleteComponent, {
      size: "sm",
    });

    modalRef.componentInstance.title = 'Delete file';
    modalRef.componentInstance.confirmText =
      'Do you want to delete this tile ' + fileName + " ?";

    modalRef.result.then(
      (res) => {
        this.executeDeleteFile(fileName);
      },
      (reason) => { }
    );
  }

  private executeDeleteFile = (name: string) => {
    this._textService.deleteFile(name)
      .subscribe({
        next: (res: any) => {
          this.getFiles();
          this._notificationService.showSuccess(res.message);
          console.info(res.message);
        },
        error: (err: any) => {
          // Handle an error
          console.error(err);
        }
      });
  }
}
