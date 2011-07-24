Ext.define('GenForm.view.product.ShapeForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.shapeform',

    createItems: function () {
        var     me = this

        return [
            me.createShapeFieldSet(),
            me.createTabPanel()
        ];
    },

    createShapeFieldSet: function () {
        var me = this;
        return {
            xtype: 'fieldset',
            title: 'Vorm Details',
            defaults: {
                width: 300
            },
            items: me.createShapeDetails()
        };
    },

    createShapeDetails: function () {
        return [
            { xtype: 'textfield', name:'ShapeName',   fieldLabel: 'Vorm Naam', margin: '10 0 10 10' }
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
                me.createPackageTab(),
                me.createUnitTab()
            ]
        };
    },

    createPackageTab: function () {
        var me = this;
        return {
            title: 'Verpakkingen',
            layout: 'fit',
            items: [ me.createPackageGrid() ]
        };
    },

    createUnitTab: function () {
        var me = this;
        return {
            title: 'Eenheden',
            layout: 'fit',
            items: [me.createUnitGrid()]
        };
    },

    createPackageGrid: function () {
        var config, me = this;

        config = {
            plugins: [ me.createRowEditor() ]
        };

        return Ext.create('GenForm.view.product.PackageGrid', config);
    },

    createRowEditor: function () {
        return Ext.create('Ext.grid.plugin.RowEditing', {
            clicksToMoveEditor: 1,
            autoCancel: true
        });
    },

    createUnitGrid: function () {
        var me = this,
            config = {
              plugins: [me.createRowEditor()]
            };

        return Ext.create('GenForm.view.product.UnitGrid', config);
    },

    getShape: function () {
        var me = this;
        return me.getFormData();
    }

});