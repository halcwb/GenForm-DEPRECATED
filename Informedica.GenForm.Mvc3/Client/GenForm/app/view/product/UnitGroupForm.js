Ext.define('GenForm.view.product.UnitGroupForm', {
    extend: 'GenForm.lib.view.ui.UnitGroupForm',
    alias: 'widget.unitform',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createPackageGrid: function () {
        var me = this;
        return me.addGrid(Ext.create('GenForm.view.product.PackageGrid'), 'PackageGrid');
    },

    createRouteGrid: function () {
        var me = this;
        return me.addGrid(Ext.create('GenForm.view.product.RouteGrid'), 'RouteGrid');
    },

    createUnitGroupGrid: function () {
        var me = this;
        return me.addGrid(Ext.create('GenForm.view.product.UnitGroupGrid'), 'UnitGroupGrid');
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

    getShape: function () {
        var me = this;
        return me.getFormData();
    }

});