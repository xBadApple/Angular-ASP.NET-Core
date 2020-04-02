import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale} from 'ngx-bootstrap';
defineLocale('pt-br', ptBrLocale);
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService

  ) { this.localeService.use('pt-br'); }

  titulo = 'Eventos';
  _filtroLista;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  modoSalvar: string;
  bodyDeletarEvento = '';
  
  file: File;

  get filtroLista(): string {
    return this._filtroLista;
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = Object.assign({}, evento);
    this.evento.imagemUrl = '';
    this.registerForm.patchValue(this.evento);
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = (this.filtroLista) ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }


  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza de que deseja excluir o Evento: ${evento.tema}?`;
  }

  confirmDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Deletado com sucesso');
      }, error => {
        this.toastr.error('Erro ao tentar Deletar')
        console.log(error);
      }
    );
  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe( (_eventos: Evento[]) => {
      this.eventos = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    } , error => {
      console.log(error);
    }
    );
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemUrl: ['', Validators.required]
    });
  }

  uploadImagem()
  {
    const nomeArquivo = this.evento.imagemUrl.split('\\');
    this.evento.imagemUrl = nomeArquivo[2];
    this.eventoService.postUpload(this.file, nomeArquivo[2]).subscribe();
  }

  salvarAlteracao(template: any) {
    if(this.registerForm.valid)
    {
      if(this.modoSalvar === 'post')
      {
        this.evento = Object.assign({}, this.registerForm.value);

        this.uploadImagem();

        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com sucesso');
          }, error => {
            this.toastr.error('Erro ao inserir')
            console.log(error);
          }
        );
      }
      else {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);

        this.uploadImagem();

        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com sucesso');
          }, error => {
            this.toastr.error('Erro ao editar')
            console.log(error);
          }
        );
      }
    }
  }

  onFileChange(event) {
    const reader = new FileReader();

    if(event.target.files && event.target.files.length) {
      this.file = event.target.files;
      console.log(this.file);
    }
  }
}

