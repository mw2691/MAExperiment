function GenerateBalanceFactors = GenerateBalanceFactors(numberOfParticipants)

for i = 1:numberOfParticipants
    SOALevels = ["SOA1", "SOA2", "SOA3", "SOA4"];
    Interactions = ["Pour", "Place"];
    Placements = ["Left", "Right"];
    nConditions = length(SOALevels) * length(Interactions) * length(Placements);
    experimentalTrials = nConditions*10;
    randomTrials = 1;
    trialsPerCondition = ceil(experimentalTrials/nConditions);
    [SOAOrdered,InteractionsOrdered, PlacementsOrdered] = BalanceFactors(trialsPerCondition, randomTrials, SOALevels, Interactions, Placements);
    
    resultName = 'BalancedFactors';
    iDToString = int2str(i);
    resultFileName=[iDToString '_' resultName '.txt'];
    resultFileHeader = 'SOA\t Interactions\t Placements\n';
    resultFileFormatSpecifier = '%s\t %s\t %s\n';
    resultFile = fopen(resultFileName, 'w');
    fprintf(resultFile, resultFileHeader);
    balancedFactors = [SOAOrdered InteractionsOrdered PlacementsOrdered];
    fprintf(resultFile, resultFileFormatSpecifier, balancedFactors');
    fclose(resultFile);
end
end
