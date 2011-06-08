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
        var me = this;
        me.items = this.createItems();

        me.callParent(arguments);
    },

    createItems: function () {
        var me = this;

        return [
            me.createProductFieldSet(),
            me.createTabPanel()
        ];
    },

    createTabPanel: function () {
        var me = this;
        return {
            xtype: 'tabpanel',
            plain: true,
            activeTab: 0,
            height: 235,
            defaults: { bodyStyle:'padding:10px' },
            items:[
                me.createSubstanceTab(),
                me.createRouteGrid(),
                me.createProductTextEditor()
            ]
        };
    },

    createSubstanceTab: function () {
        var me = this;
        return {
            title: 'Stoffen',
            layout: 'fit',
            items: [ me.createSubstanceGrid() ]
        };
    },

    createSubstanceGrid: function () {
        var config, me = this;

        config = {
            plugins: [ me.createRowEditor() ]
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
        var me = this;
        return {
            xtype: 'fieldset',
            title: 'Artikel Details',
            defaults: {
                width: 400
            },
            items: me.createProductDetails()
        };
    },

    createProductDetails: function () {
        return [
            { xtype: 'textfield',    name:'ProductName',   fieldLabel: 'Artikel Naam', margin: '10 0 10 10' },
            { xtype: 'textfield',    name: 'ProductCode',  fieldLabel: 'Artikel Code', margin: '10 0 10 10' },
            { xtype: 'editcombo',    name: 'GenericName',  fieldLabel: 'Generiek',     margin: '10 0 10 10',  displayField: 'GenericName',  store: 'product.GenericName', queryMode: 'local'},
            { xtype: 'editcombo',    name: 'BrandName',    fieldLabel: 'Merk',         margin: '10 0 10 10',  displayField: 'BrandName',    store: 'product.BrandName',   queryMode: 'local'},
            { xtype: 'editcombo',    name: 'ShapeName',    fieldLabel: 'Vorm',         margin: '10 0 10 10',  displayField: 'ShapeName',    store: 'product.ShapeName' ,  queryMode: 'local'},
            { xtype: 'numberfield',  name: 'Quantity',     fieldLabel: 'Hoeveelheid',  margin: '10 0 10 10' },
            { xtype: 'editcombo',    name: 'UnitName',     fieldLabel: 'Eenheid',      margin: '10 0 10 10',  displayField: 'UnitName',     store: 'product.UnitName',    queryMode: 'local' },
            { xtype: 'editcombo',    name: 'PackageName',  fieldLabel: 'Verpakking',   margin: '10 0 10 10' , displayField: 'PackageName',  store: 'product.PackageName', queryMode: 'local'}
        ];
    },

    getProduct: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }

});
