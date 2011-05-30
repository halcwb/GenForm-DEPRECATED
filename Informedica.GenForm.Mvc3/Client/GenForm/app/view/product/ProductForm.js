/**
 * Created by .
 * User: hal
 * Date: 26-4-11
 * Time: 10:24
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductForm', {
    extend: 'Ext.form.FormPanel',
    alias: 'widget.productform',

    waitMsgTarget: true,
    
    initComponent: function () {
        this.items = this.createItems();

        this.callParent();
    },

    createItems: function () {
        return [
            this.createProductFieldSet(),
            this.createTabPanel()
        ];
    },

    createTabPanel: function () {
        return {
            xtype: 'tabpanel',
            plain: true,
            activeTab: 0,
            height: 235,
            defaults: { bodyStyle:'padding:10px' },
            items:[
                this.createSubstanceTab(),
                this.createRouteGrid(),
                this.createProductTextEditor()
            ]
        };
    },

    createSubstanceTab: function () {
      return {
        title: 'Stoffen',
        layout: 'fit',
        items: [ this.createSubstanceGrid() ]
      };
    },

    createSubstanceGrid: function () {
        var config;

        config = {
            plugins: [ this.createRowEditor() ]
        };

        return Ext.create('GenForm.view.product.ProductSubstanceGrid', config);
    },

    
    createRowEditor: function () {
        return Ext.create('Ext.grid.plugin.RowEditing', {
            clicksToMoveEditor: 1,
            autoCancel: false
        });
    },

    createRouteGrid: function () {
        return {
            title:'Routes',
            defaults: {width: 230},
            defaultType: 'textfield',

            items: [
                {
                    fieldLabel: 'Routenaam',
                    name: 'routename'
                },
                {
                    fieldLabel: 'Afkorting',
                    name: 'routeabbreviation'
                }
            ]
        };
    },

    createProductTextEditor: function () {
        return {
            cls: 'x-plain',
            title: 'Bijzonderheden',
            layout: 'fit',
            items: {
                xtype: 'htmleditor',
                name: 'text',
                fieldLabel: 'Bijzonderheden'
            }
        }
    },

    createProductFieldSet: function () {
        return {
            xtype: 'fieldset',
            title: 'Artikel Details',
            defaults: {
                width: 400
            },
            items: this.createProductDetails()
        };
    },

    createProductDetails: function () {
        return [
            { xtype: 'textfield',    name:'ProductName',   fieldLabel: 'Artikel Naam', margin: '10 0 10 10' },
            { xtype: 'textfield',    name: 'ProductCode',  fieldLabel: 'Artikel Code', margin: '10 0 10 10' },
            { xtype: 'editcombo',    name: 'GenericName',  fieldLabel: 'Generiek',     margin: '10 0 10 10',  displayField: 'GenericName',  store: 'product.GenericName'},
            { xtype: 'editcombo',    name: 'BrandName',    fieldLabel: 'Merk',         margin: '10 0 10 10',  displayField: 'BrandName',    store: 'product.GenericName'},
            { xtype: 'editcombo',    name: 'ShapeName',    fieldLabel: 'Vorm',         margin: '10 0 10 10',  displayField: 'ShapeName',    store: 'product.GenericName' },
            { xtype: 'numberfield',  name: 'Quantity',     fieldLabel: 'Hoeveelheid',  margin: '10 0 10 10' },
            { xtype: 'editcombo',    name: 'UnitName',     fieldLabel: 'Eenheid',      margin: '10 0 10 10',  displayField: 'UnitName',     store: 'product.GenericName' },
            { xtype: 'editcombo',    name: 'PackageName',  fieldLabel: 'Verpakking',   margin: '10 0 10 10' , displayField: 'PackageName',  store: 'product.GenericName'}
        ];
    },

    getProduct: function () {
        var record = this.getRecord();
        this.getForm().updateRecord(record);
        return record;
    }

});
