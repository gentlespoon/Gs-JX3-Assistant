import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  Renderer2,
} from '@angular/core';
import { Item } from 'src/app/models/item/item/Item';
import { ItemInfoFloatService } from 'src/app/services/items/item-info-float/item-info-float.service';
import { Router } from '@angular/router';
import { faExternalLinkAlt, faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-item-info-float',
  templateUrl: './item-info-float.component.html',
  styleUrls: ['./item-info-float.component.scss'],
})
export class ItemInfoFloatComponent implements OnInit {
  @Input() item: Item;
  @Input() X: number;
  @Input() Y: number;

  @ViewChild('float') float: ElementRef;
  @ViewChild('title') title: ElementRef;

  constructor(
    private renderer: Renderer2,
    private itemInfoFloatService: ItemInfoFloatService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    console.log(this.X, this.Y);
    this.renderer.setStyle(this.float.nativeElement, 'left', this.X + 'px');
    this.renderer.setStyle(this.float.nativeElement, 'top', this.Y + 'px');
    this.renderer.setStyle(
      this.title.nativeElement,
      'background-image',
      `url('/assets/images/itemIcons/${this.item.id}.gif')`
    );
  }

  public close(): void {
    this.itemInfoFloatService.remove(this.item);
  }

  // fontAwesome
  public faExternalLinkAlt = faExternalLinkAlt;
  public faTimes = faTimes;
}
