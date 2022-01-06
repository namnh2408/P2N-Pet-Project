import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChangeEnumToList } from '../../../heplers/utils';
import { StatusNormal } from '../../../models/status';
import { ContactService } from '../../../services/contact.service';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.scss']
})
export class UpdateContactComponent implements OnInit {

  id: string;
  form: FormGroup;
  loading = false;
  submitted = false;
  firstload = false;

  contactStatusText = StatusNormal;
  contactStatusOptions = []

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactService,
    private modalService: NgbModal) {
    this.buildSelection();
  }

  ngOnInit() {
    this.firstload = true;
    this.id = this.route.snapshot.params['id'];

    this.contactService.GetDetailContact(this.id)
    .subscribe((x: any) => {
      var contact = x.content.Contact;
      this.f.Name.setValue(contact.Name);
      this.f.Email.setValue(contact.Email);
      this.f.Phone.setValue(contact.Phone);
      this.f.Subject.setValue(contact.Subject);
      this.f.Content.setValue(contact.Content);
      this.f.Status.setValue(contact.Status);
      this.firstload = false;
    });

    this.form = this.formBuilder.group({
      Name: ['', Validators.required],
      Email: ['', Validators.required],
      Phone: ['', Validators.required],
      Subject: ['', Validators.required],
      Content: ['', Validators.required],
      Status: [10, Validators.required]
    });
  }

  get f() { return this.form.controls; }

  ngAfterViewInit() {

  }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.contactService.UpdateContact({ Id: this.id, ...this.form.value })
    .subscribe(() => {
      this.router.navigate(["admin/list-contact"]);
    }, error => {
      this.loading = false;
    });
  }

  buildSelection() {
    ChangeEnumToList(this.contactStatusText, this.contactStatusOptions);
  }

}
