/**
 * Created by .
 * User: hal
 * Date: 8-5-11
 * Time: 18:33
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.view.product.ProductSubstanceGrid', function () {
    var getProductSubstanceTestStore, createProductSubstanceTestStore, getProductSubstanceGrid,
        matchDataIndexWithModel, matchModelWithDataIndex, findRecordByGenericName ;

    createProductSubstanceTestStore = function () {
        return Ext.create(GenForm.data.ProductSubstanceTestData, { storeId: 'productSubstanceTestStore'});
    };

    getProductSubstanceGrid = function () {
        var grid, rowEditor;

        rowEditor = Ext.create('Ext.grid.plugin.RowEditing', {
            clicksToMoveEditor: 1,
            autoCancel: false
        });

        grid = Ext.create('GenForm.view.product.ProductSubstanceGrid', {
            id: 'productSubstanceTestGrid',
            selModel: 'rowmodel',
            store: getProductSubstanceTestStore(),
            plugins: [rowEditor]
        });
        grid.getRowEditor = function () { return grid.plugins[0]; };
        return grid;
    };

    getProductSubstanceTestStore = function () {
        if(!Ext.data.StoreManager.lookup('productSubstanceTestStore')) {
            createProductSubstanceTestStore();
        }
        return Ext.data.StoreManager.lookup('productSubstanceTestStore');
    };

    matchDataIndexWithModel = function (grid) {
        var model = grid.getStore().first(), isMatch = true, column;

        for (column in grid.columns)  {
            isMatch = matchModelWithDataIndex(grid.columns[column].dataIndex, model);
            if (!isMatch) {
                break;
            }
        }
        return isMatch;
    };

    matchModelWithDataIndex = function (dataIndex, model) {
        var item;

        for (item in model.data) {
            if (item === dataIndex) {
                return true;
            }
        }

        return false;
    };

    findRecordByGenericName = function (grid, value) {
        return grid.store.findRecord('GenericName', value);
    };

    it('StoreManager contains a productSubstanceTestStore', function  () {
        expect(getProductSubstanceTestStore()).toBeDefined();
    });

    it('Should contain a productSubstanceTestStore', function () {
        expect(getProductSubstanceGrid().getStore().storeId === 'productSubstanceTestStore').toBe(true);
    });

    it('selectionmodel of grid should be rowmodel', function () {
        expect(getProductSubstanceGrid().getSelectionModel().alias === 'selection.rowmodel').toBe(true);
    });

    it('grid should contain one row from teststore', function () {
        expect(getProductSubstanceGrid().getStore().count() > 0).toBeTruthy();
    });

    it('dataIndex items should match model fields', function () {
        expect(matchDataIndexWithModel(getProductSubstanceGrid())).toBeTruthy();
    });

    it('contains a rowEditor plugin', function () {
        expect(getProductSubstanceGrid().getRowEditor()).toBeDefined();
    });

    it('first data row can be retrieved', function () {
        expect(getProductSubstanceGrid().store.first()).toBeDefined();
    });

    it('first data row can be changed', function () {
        var record = getProductSubstanceGrid().store.first();
        record.data.GenericName = 'changed';

        expect(getProductSubstanceGrid().store.first().data.GenericName).toBe('changed');
    });

    it('a second data row can be added', function () {
        var record = { OrderNumber: '2', GenericName: 'codeine', Quantity: '20', Unit: 'mg' };
        getProductSubstanceGrid().store.add(record);

        expect(getProductSubstanceGrid().store.count()).toBe(2);
    });

    it('the second data row can be found', function () {
        expect(findRecordByGenericName(getProductSubstanceGrid(), 'codeine')).toBeDefined();
    });

    it('a second data row can be deleted', function () {
        var record = findRecordByGenericName(getProductSubstanceGrid(), 'codeine');
        getProductSubstanceGrid().store.remove(record);
        expect(getProductSubstanceGrid().store.count()).toBe(1);
    });

});