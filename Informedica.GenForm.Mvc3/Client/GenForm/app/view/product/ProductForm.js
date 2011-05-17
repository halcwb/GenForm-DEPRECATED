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

    initComponent: function () {
        this.items = this.createItems();

        this.callParent(arguments);
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
            items: this.createProductDetails()
        };
    },

    createProductDetails: function () {
        return [
            { xtype: 'textfield', fieldLabel: 'Artikel Naam', name:'productname', margin: '10 0 10 10' },
            { xtype: 'textfield', fieldLabel: 'Artikel Code', name: 'productcode', margin: '0 0 10 10' },
            { xtype: 'combobox',  displayField: 'GenericName', fieldLabel: 'Generiek', store: 'GenForm.store.product.GenericNameStore', margin: '0 0 10 10' },
            { xtype: 'combobox',  displayField: 'BrandName', fieldLabel: 'Merk', store: 'GenForm.store.product.GenericNameStore', margin: '0 0 10 10' },
            { xtype: 'combobox',  displayField: 'ShapeName', fieldLabel: 'Vorm', store: 'GenForm.store.product.GenericNameStore', margin: '0 0 10 10' },
            { xtype: 'numberfield', fieldLabel: 'Hoeveelheid', name: 'Quantity', margin: '0 0 10 10' },
            { xtype: 'combobox',  displayField: 'UnitName', fieldLabel: 'Eenheid', store: 'GenForm.store.product.GenericNameStore', margin: '0 0 10 10' },
            { xtype: 'combobox',  displayField: 'PackageName', fieldLabel: 'Verpakking', store: 'GenForm.store.product.GenericNameStore', margin: '0 0 10 10' }
        ];
    }

});
