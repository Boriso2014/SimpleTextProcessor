import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { UntypedFormControl, UntypedFormGroup, AbstractControl, Validators } from "@angular/forms";

@Component({
  selector: 'app-open-file',
  templateUrl: './open-file.component.html',
  styleUrls: ['./open-file.component.scss']
})
export class OpenFileComponent implements OnInit {
  public fileToUpload: File | undefined;
  public fileName: string = "Choose file";
  public isFileValid: boolean = false;
  public openTxtFileForm!: UntypedFormGroup;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.openTxtFileForm = new UntypedFormGroup({
      chooseFile: new UntypedFormControl("", Validators.required),
    });
  }

  onFileChange(event: any) {
    const file: File = event.target.files[0] as File;
     if (file) {
       this.fileToUpload = file;
       this.fileName = this.fileToUpload.name;
       this.isFileValid = this.isSelectedFileValid();
     }
     else {
       this.fileToUpload = undefined;
       this.fileName = '';
       this.isFileValid = false;
     }
  }

  public submitForm = () => {
    this.activeModal.close(this.fileToUpload);
  };

  public closeForm() {
    this.activeModal.dismiss();
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.openTxtFileForm.controls[controlName].hasError(errorName);
  };

  private isSelectedFileValid = (): boolean => {
    const fileName: string = this.fileName;
    const control: AbstractControl = this.openTxtFileForm.controls["chooseFile"];

    if (fileName.length == 0) {
      control.setErrors({ requered: true });
      return false;
    }

    if (fileName.match(".txt$") == null) {
      control.setErrors({ invalidFileType: true });
      return false;
    }
    return true;
  }
}
