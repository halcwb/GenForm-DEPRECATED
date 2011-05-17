Ext.define('GenForm.data.ProductSubstanceTestData', {
    extend: 'Ext.data.Store',
    storeId: 'productSubstanceTestStore',
    fields: [ 'OrderNumber', 'GenericName', 'Quantity', 'Unit'],

    model: 'GenForm.model.product.ProductSubstanceModel',

    data: {
        'items' : [
            { 'OrderNumber': '1', 'GenericName': 'paracetamol', 'Quantity': '500', 'Unit': 'mg' }
        ]
    },

    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            root: 'items'
        }
    }
});

Ext.define('GenForm.view.product.ProductSubstanceGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productsubstancegrid',

    testStore: 'GenForm.data.ProductSubstanceTestData',
    productionStore: 'GenForm.store.product.ProductSubstanceStore',

    initComponent: function () {
        this.store = this.store || this.getProductSubstanceStore();
        this.columns = this.createColumns();

        this.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'ordernumber', header: 'Volgorde', dataIndex: 'OrderNumber', field: 'numberfield'},
            { id: 'genericname', header: 'Generiek', dataIndex: 'GenericName', field: 'textfield'},
            { id: 'quantity', header: 'Hoeveelheid', dataIndex: 'Quantity', field: 'textfield'},
            { id: 'unit', header: 'Eenheid', dataIndex: 'Unit', field: 'textfield'}
        ];
    },

    getProductSubstanceStore: function () {
        return Ext.create(this.testStore);
    }
});

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
            { xtype: 'textfield', fieldLabel: 'Artikel Code', name: 'productcode', margin: '0 0 10 10' }        ];
    }

});


Ext.define('GenForm.view.product.ProductWindow', {
    extend: 'Ext.Window',
    alias: 'widget.productwindow',

    width: 500,
    height: 500,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createProductForm();

        this.callParent(arguments);
    },

    createProductForm: function () {
        return { xtype: 'productform' };
    }
});

Ext.create('GenForm.view.product.ProductWindow').show();

