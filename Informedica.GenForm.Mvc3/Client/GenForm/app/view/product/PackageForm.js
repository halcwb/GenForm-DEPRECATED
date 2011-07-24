Ext.define('GenForm.view.product.PackageForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.packageform',

    createItems: function () {
        var me = this;

        return [
            me.createPackageFieldSet(),
            me.createTabPanel()
        ];
    },

    createPackageFieldSet: function () {
        var me = this;
        return {
            xtype: 'fieldset',
            title: 'Verpakking Details',
            defaults: {
                width: 300
            },
            items: me.createPackageDetails()
        };
    },

    createPackageDetails: function () {
        return [
            { xtype: 'textfield', name:'PackageName',   fieldLabel: 'Verpakking Naam', margin: '10 0 10 10' }
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
                me.createShapeTab(),
                me.createUnitTab()
            ]
        };
    },

    createShapeTab: function () {
        var me = this;
        return {
            title: 'Vormen',
            layout: 'fit',
            items: [ me.createShapeGrid() ]
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

    createShapeGrid: function () {
        var config, me = this;

        config = {
            plugins: [ me.createRowEditor() ]
        };

        return Ext.create('GenForm.view.product.ShapeGrid', config);
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

    getPackage: function () {
        var me = this;
        return me.getFormData();
    }

});