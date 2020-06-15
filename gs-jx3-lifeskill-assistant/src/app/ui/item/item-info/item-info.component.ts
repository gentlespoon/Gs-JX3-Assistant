import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/models/Item/item';
import { ItemInfoService } from 'src/app/services/itemInfo/item-info.service';
import { ColorService } from 'src/app/services/color/color.service';
import { SourceService } from 'src/app/services/source/source.service';
import { Source } from 'src/app/models/source/source';
import { SkillService } from 'src/app/services/skill/skill.service';

@Component({
  selector: 'app-item-info',
  templateUrl: './item-info.component.html',
  styleUrls: ['./item-info.component.scss'],
})
export class ItemInfoComponent implements OnInit {
  constructor(
    private itemInfoService: ItemInfoService,
    private colorService: ColorService,
    private sourceService: SourceService,
    private skillService: SkillService
  ) {}

  ngOnInit(): void {}

  close() {
    this.itemInfoService.itemId = null;
  }

  // itemInfoService passthrough
  public get item(): Item {
    return this.itemInfoService.item;
  }
  // colorService passthrough
  public get colors(): string[] {
    return this.colorService.colors;
  }
  // sourceService passthrough
  public get sources(): Source[] {
    return this.sourceService.sources;
  }

  public getSourceByAbbr(abbr: string): Source {
    return this.sourceService.getSourceByAbbr(abbr);
  }

  public isCraftable(abbr: string[]): boolean {
    for (let abb of abbr) {
      try {
        this.skillService.getSkillByAbbr(abb);
        return true;
      } catch {}
    }
    return false;
  }

  public craft(itemId: number, skill: string[]) {
    this.close();
    console.log('Craft ', itemId, skill);
  }
}
