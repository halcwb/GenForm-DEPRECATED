Ext.define('GenForm.view.product.PackageForm', {
    extend: 'GenForm.lib.view.ui.PackageForm',
    alias: 'widget.packageform',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createShapeGrid: function () {
        var me = this;
        return me.addGrid(Ext.create('GenForm.view.product.ShapeGrid'), 'ShapeGrid');
    },

    addGrid: function (grid, name) {
        var me = this;
        if (!me.grids) me.grids = {};
        me.grids[name] = grid;

        return grid;
    },

    // ToDo: temp hack to get rid of load mask
    onTabChangeLoadStore: function (panel, newTab) {
        newTab.store.load();
    },

    getPackage: function () {
        var me = this;
        return me.getFormData();
    }

});