import { Component, OnInit } from '@angular/core';
import { FilterService } from '../Services/filter.service';
import { FormBuilder } from '@angular/forms';
import { Table } from '../models/Table';
import { Colum } from '../models/Colum';
import { NONE_TYPE } from '@angular/compiler';
import { Filter } from '../models/Filter';
import { DisplayField } from '../models/DisplayField';
import { FieldFilter } from '../models/FieldFilter';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css'],
})
export class FilterComponent implements OnInit {
  filterForm = this.fb.group({
    projectName: [''],
    tableOfFilter: [''],
    columsName: [''],
    filterType: [''],
    number2: [''],
    valueOfFilter: [''],
  });
  typeOfFielde: string;
  fieldType: string;
  displayDateInput: boolean;
  filterTypes: Array<Colum> = [];
  companycode: string;
  projectsName: string;
  tableList: string[];
  tableName: Array<Table> = [];
  projectCode: string;
  columNameFilter: string;
  displayNumberInput: boolean;
  displayString: boolean;
  displayInt: boolean;
  displayDate: boolean;
  displayBool: boolean;
  columNameForFilter: Array<Colum>;
  filterTypesstr: any;
  selectedFilter: string;
  selectedFilters: Filter[];
  filterToSend: Filter[];
  flage: string;
  newFieldFilter: Filter;
  arrayOfSelectedFilter: Array<Filter>;
  arrayOfName: Array<string>;
  displayField: Array<DisplayField>;
  static counter: number = 1;
  onLabel: string;
  offLabel: string;
  filterDialog: boolean;
  nameDialog: boolean;
  constructor(
    private FS: FilterService,
    private fb: FormBuilder,
    private router: Router,public datepipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.displayNumberInput = false;
    this.offLabel = 'כן';
    this.onLabel = 'לא';
    //  this.displayDate=false;
    //  this.displayInt=false;
    this.displayDateInput = true;
    this.companycode = localStorage.getItem('companyCode');
    this.projectName();
    this.arrayOfSelectedFilter = new Array<Filter>();
    this.displayField = new Array<DisplayField>();
    let dis = new DisplayField();
    dis.name = 'string';
    dis.value = false;
    this.displayField.push(dis);
    let dis1 = new DisplayField();
    dis1.name = 'number';
    dis1.value = false;
    this.displayField.push(dis1);
    let dis2 = new DisplayField();
    dis2.name = 'date';
    dis2.value = false;
    this.displayField.push(dis2);
    let dis3 = new DisplayField();
    dis3.name = 'bool';
    dis3.value = false;
    this.displayField.push(dis3);
    this.filterDialog = false;
    this.nameDialog = false;
    this.getFilter();
  }
  getFilter() {
    return this.FS.getFilter(
      parseInt(localStorage.getItem('projectCode'))
    ).subscribe((result) => {
      debugger;
      result.forEach((element) => {
        debugger;
        let filter = new Filter();
        filter.ProjectCode = parseInt(localStorage.getItem('projectCode'));
        filter.id = element.id;
        filter.TableName = element.tableName;
        filter.FilterList = new Array<FieldFilter>();
        element.lstField.forEach((element) => {
          let fieldFilter = new FieldFilter();
          fieldFilter.FieldName = element.fieldName;
          fieldFilter.FieldType = element.fieldType;
          fieldFilter.FilterType = element.filterType;
          switch (fieldFilter.FieldType) {
            case 'varchar':
            case 'nvarchar':
              fieldFilter.Filter = element.stringValue;
              break;
            case 'int':
            case 'decimal':
            case 'float':
            case 'money':
              fieldFilter.Filter = element.intValue;
              break;
            case 'date':
            case 'datetime':
              fieldFilter.Filter =this.datepipe.transform( new Date(element.dateValue), 'dd/MM/yyyy');
              
               break;
            case 'bit':
              fieldFilter.Filter = element.boolValue;
              break;
            default:
              break;
          }
          // fieldFilter.Filter=element.intValue;
          filter.FilterList.push(fieldFilter);
        });
        this.arrayOfSelectedFilter.push(filter);
      });
    });
  }
  projectName() {
    debugger;
    return this.FS.projectName(this.companycode).subscribe((result) => {
      this.projectsName = result;
      // this.disable=true;
    });
  }
  getTablesName() {
    debugger;
    return this.FS.getTablesName(
      this.companycode,
      this.filterForm.controls.projectName.value.projectname
    ).subscribe((result) => {
      debugger;
      this.tableList = result.tableList;
      this.tableName = new Array<Table>();
      for (let index = 0; index < this.tableList.length; index++) {
        table: Table;
        let table = new Table();
        table.Name = this.tableList[index];
        this.tableName.push(table);
      }
      localStorage.setItem('projectCode', result.projectCode);
      this.projectCode = localStorage.getItem('projectCode');
    });
  }
  getColumnName() {
    debugger;
    return this.FS.getColumnName(
      this.projectCode,
      this.filterForm.controls.tableOfFilter.value.Name
    ).subscribe((result) => {
      this.columNameFilter = result;
      this.columNameForFilter = new Array<Colum>();
      for (let index = 0; index < this.columNameFilter.length; index++) {
        let colum = new Colum();
        colum.Name = this.columNameFilter[index];
        this.columNameForFilter.push(colum);
      }
    });
  }
  //  saveFilter(){
  //   let nameTableOfFilter:string;
  //   debugger;
  //   nameTableOfFilter=this.filterForm.controls.tableOfFilter.value.Name;
  //   const projectObj= {
  //     code: this.projectCode,
  //     tableNameFilter:nameTableOfFilter,
  //     columnsForSave: this.filterForm.controls.columsName.value
  //   };
  //   const myObjStr = JSON.stringify(projectObj);
  //   var projectJson=JSON.parse( myObjStr );
  //   return this.FS.saveFilter(projectJson)
  //   .subscribe((result)=> {
  //     alert(result);
  //   });
  // };
  getFilterTypes() {
    debugger;
    let filForm: any;
    filForm = this.filterForm.controls;
    return this.FS.getFilterTypes(
      this.projectCode,
      filForm.tableOfFilter.value.Name,
      filForm.columsName.value.Name
    ).subscribe((result) => {
      debugger;
      this.filterTypesstr = result.filterTypeList;
      this.filterTypes = new Array<Colum>();
      for (let index = 0; index < this.filterTypesstr.length; index++) {
        let colum = new Colum();
        colum.Name = this.filterTypesstr[index];
        this.filterTypes.push(colum);
      }
      this.fieldType = result.fieldType;
      debugger;
      switch (this.fieldType) {
        case 'varchar':
        case 'nvarchar':
          this.typeOfFielde = 'string';
          break;
        case 'int':
        case 'decimal':
        case 'float':
        case 'money':
          this.typeOfFielde = 'number';
          break;
        case 'date':
        case 'datetime':
          this.typeOfFielde = 'date';
          break;
        case 'bit':
          this.typeOfFielde = 'bool';
          break;
        default:
          break;
      }
      for (var i = 0; i < this.displayField.length; i++) {
        debugger;
        if (this.displayField[i].name == this.typeOfFielde)
          this.displayField[i].value = true;
        else this.displayField[i].value = false;
      }
    });
  }
  getFilterInput() {
    debugger;
    this.selectedFilter = this.filterForm.controls.filterType.value.Name;
    if (this.typeOfFielde == 'number') {
      if (this.selectedFilter == 'טווח ערכים') this.displayNumberInput = true;
      else this.displayNumberInput = false;
    } else {
      if (this.typeOfFielde == 'date')
        if (this.selectedFilter == 'בין התאריכים')
          this.displayDateInput = false;
        else this.displayDateInput = true;
    }
  }
  filter() {
    debugger;
    if (this.selectedFilters.length > 0 && this.arrayOfSelectedFilter) {
      this.filterToSend = this.arrayOfSelectedFilter.filter((val) =>
        this.selectedFilters.includes(val)
      );
      this.selectedFilters = null;
      return this.FS.getNames(this.filterToSend[0]).subscribe((result) => {
        debugger;
        this.arrayOfName = new Array();
        if (result.length > 0) {
          result.forEach((element) => {
            this.arrayOfName.push(element);
          });
        } else {
          this.arrayOfName.push('אין תוצאות המתאימות לסינון');
        }
        this.nameDialog = true;
      });
    } else alert('יש לבחור סינון');
  }
  saveFilter() {
    debugger;
    let filter = new Filter();
    // filter.id=FilterComponent.counter;
    filter.ProjectCode = parseInt(localStorage.getItem('projectCode'));
    filter.TableName = this.filterForm.controls.tableOfFilter.value.Name;
    filter.FilterList = new Array<FieldFilter>();
    let fieldFilter = new FieldFilter();
    fieldFilter.FieldName = this.filterForm.controls.columsName.value.Name;
    fieldFilter.FieldType = this.fieldType;
    fieldFilter.FilterType = this.selectedFilter;
    if (fieldFilter.FilterType == 'טווח ערכים') {
      fieldFilter.Filter =
        this.filterForm.controls.valueOfFilter.value +
        '-' +
        this.filterForm.controls.number2.value;
    } else fieldFilter.Filter = this.filterForm.controls.valueOfFilter.value;
    debugger;
    filter.FilterList.push(fieldFilter);

    let projectName = this.filterForm.controls.projectName.value;
    Object.keys(this.filterForm.controls).forEach((key) => {
      this.filterForm.controls[key].reset();
    });
    this.displayField.forEach((element) => {
      element.value = false;
    });
    this.filterForm.controls.projectName.setValue(projectName);
    FilterComponent.counter++;
    return this.FS.saveFilter(filter).subscribe((result) => {
      debugger;
      filter.id = result;
      this.arrayOfSelectedFilter.push(filter);
    });
  }
  deleteFilter(filter: Filter) {
    return this.FS.deleteFilter(filter.id).subscribe((resulte) => {
      if (resulte)
        this.arrayOfSelectedFilter = this.arrayOfSelectedFilter.filter(
          (val) => val.id !== filter.id
        );
    });
  }
  externalFilter(filter: Filter) {
    debugger;
    this.newFieldFilter = { ...filter };
    this.filterDialog = true;
    return this.FS.getColumnName(
      this.projectCode,
      this.newFieldFilter.TableName
    ).subscribe((result) => {
      this.columNameFilter = result;
      this.columNameForFilter = new Array<Colum>();
      for (let index = 0; index < this.columNameFilter.length; index++) {
        let colum = new Colum();
        colum.Name = this.columNameFilter[index];
        this.columNameForFilter.push(colum);
        debugger;
        let tableName = new Table();
        tableName.Name = this.newFieldFilter.TableName;
        this.filterForm.controls.tableOfFilter.setValue(tableName);
      }
    });
  }
  hideDialog() {
    this.filterDialog = false;
    this.nameDialog = false;
  }
  emailComponent() {
    this.router.navigate(['/sendEmail']);
  }
  saveFieldFilter() {
    let field = new FieldFilter();
    field.FieldName = this.filterForm.controls.columsName.value.Name;
    field.FieldType = this.fieldType;
    field.FilterType = this.selectedFilter;
    if (field.FilterType == 'טווח ערכים') {
      field.Filter =
        this.filterForm.controls.valueOfFilter.value +
        '-' +
        this.filterForm.controls.number2.value;
    } else field.Filter = this.filterForm.controls.valueOfFilter.value;
    let index = this.arrayOfSelectedFilter
      .map(function (e) {
        return e.id;
      })
      .indexOf(this.newFieldFilter.id);
    this.arrayOfSelectedFilter[index].FilterList.push(field);
    this.FS.saveFilter(this.arrayOfSelectedFilter[index]).subscribe(
      (result) => {
        let a: Number;
        a = result;
      }
    );
    this.filterDialog = false;
  }
}
