Ext.define('GenForm.view.product.ProductForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.productform',

    height: 600,
    
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
            dockedItems: {
                xtype: 'toolbar',
                dock: 'top',
                items: [
                    { text: 'Voeg stof toe', action: 'newproductsubstance' }
                ]
            },
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
                width: 600
            },
            items: me.createProductDetails()
        };
    },

    createProductDetails: function () {
        var me = this, margin = '10 0 10 10';
        return [
            { xtype: 'textfield',    name:'ProductName',   fieldLabel: 'Artikel Naam', margin: margin},
            { xtype: 'textfield',    name: 'ProductCode',  fieldLabel: 'Artikel Code', margin: margin },
            me.createGenericNameCombo(margin),
            me.createBrandNameCombo(margin),
            me.createShapeNameCombo(margin),
            { xtype: 'numberfield',  name: 'Quantity',     fieldLabel: 'Hoeveelheid',  margin: margin },
            me.createUnitNameCombo(margin),
            me.createPackageNameCombo(margin)
        ];
    },

    createGenericNameCombo: function (margin) {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'GenericName',
            fieldLabel: 'Generiek',
            margin: margin,
            displayField: 'GenericName',
            store: 'product.GenericName'
        });

        return combo;
    },

    createBrandNameCombo: function (margin) {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'BrandName',
            fieldLabel: 'Merk',
            margin: margin,
            displayField: 'BrandName',
            store: 'product.BrandName'
        });

        return combo;
    },

    createShapeNameCombo: function (margin) {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'ShapeName',
            fieldLabel: 'Vorm',
            margin: margin,
            displayField: 'ShapeName',
            store: 'product.ShapeName'
        });

        return combo;
    },

    createUnitNameCombo: function (margin) {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'UnitName',
            fieldLabel: 'Eenheid',
            margin: margin,
            displayField: 'UnitName',
            store: 'product.UnitName'
        });

        return combo;
    },

    createPackageNameCombo: function (margin) {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'PackageName',
            fieldLabel: 'Verpakking',
            margin: margin ,
            displayField: 'PackageName',
            store: 'product.PackageName'
        });

        return combo;
    },

    getProduct: function () {
        var me = this;
        return me.getFormData()
    }

});
