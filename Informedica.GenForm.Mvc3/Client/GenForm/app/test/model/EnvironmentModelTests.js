Ext.define('GenForm.test.model.EnvironmentModelTests', {
    describe: 'EnvironmentModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
            if (!model) model = Ext.create('GenForm.model.environment.Environment');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have an Id field', function () {
            expect(model.data.Id).toBeDefined();
        });

        it('have an Environment field', function () {
            expect(model.data.Environment).toBeDefined();
        });

        it('have a Log Path field', function () {
            expect(model.data.LogPath).toBeDefined();
        });

        it('have a Export Path field', function () {
            expect(model.data.ExportPath).toBeDefined();
        });

    }
});